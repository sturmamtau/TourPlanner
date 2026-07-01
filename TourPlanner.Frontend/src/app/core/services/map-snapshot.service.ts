import { Injectable } from '@angular/core';
import * as L from 'leaflet';
import leafletImage from 'leaflet-image';

@Injectable({ providedIn: 'root' })
export class MapSnapshotService {

  /**
   * Rendert eine Route (GeoJSON) unsichtbar und liefert sie als PNG-Blob zurück.
   * Wird nur einmalig beim Erstellen einer Tour verwendet - keine Interaktivität.
   */
  async createSnapshot(geoJson: GeoJSON.GeoJsonObject): Promise<Blob> {
    // Temporären, unsichtbaren Container erzeugen
    const container = document.createElement('div');
    container.style.width = '800px';
    container.style.height = '500px';
    container.style.position = 'absolute';
    container.style.left = '-9999px'; // außerhalb des sichtbaren Bereichs
    document.body.appendChild(container);

    try {
      const map = L.map(container, {
        zoomControl: false,
        attributionControl: false,
      });

      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(map);

      const routeLayer = L.geoJSON(geoJson, {
        style: { color: '#3388ff', weight: 4 },
      }).addTo(map);

      const bounds = routeLayer.getBounds();
      map.fitBounds(bounds, { padding: [20, 20] });

      // Warten, bis alle Kacheln geladen sind, bevor exportiert wird
      await this.waitForTiles(map);

      const blob = await this.exportCanvas(map);

      map.remove();
      return blob;
    } finally {
      document.body.removeChild(container);
    }
  }

  private waitForTiles(map: L.Map): Promise<void> {
    return new Promise((resolve) => {
      let pending = 0;
      let loaded = false;

      map.eachLayer((layer) => {
        if (layer instanceof L.TileLayer) {
          pending++;
          layer.on('load', () => {
            pending--;
            if (pending === 0 && !loaded) {
              loaded = true;
              resolve();
            }
          });
        }
      });

      // Fallback, falls 'load' aus irgendeinem Grund nicht feuert
      setTimeout(() => {
        if (!loaded) {
          loaded = true;
          resolve();
        }
      }, 5000);
    });
  }

  private exportCanvas(map: L.Map): Promise<Blob> {
    return new Promise((resolve, reject) => {
      leafletImage(map, (err: Error, canvas: HTMLCanvasElement) => {
        if (err) {
          reject(err);
          return;
        }
        canvas.toBlob((blob) => {
          if (blob) resolve(blob);
          else reject(new Error('Canvas export fehlgeschlagen'));
        }, 'image/png');
      });
    });
  }
}