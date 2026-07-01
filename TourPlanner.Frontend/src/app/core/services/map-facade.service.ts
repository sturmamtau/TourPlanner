// src/app/services/map-facade.service.ts
import { Injectable } from '@angular/core';
import * as L from 'leaflet';

@Injectable({ providedIn: 'root' })
export class MapFacadeService {
  private map: L.Map | null = null;
  private routeLayer: L.GeoJSON | null = null;
  private markerLayer: L.Marker | null = null;

  initMap(containerId: string): void {
    if (this.map) return;

    this.map = L.map(containerId, {
      zoomControl: true,
      attributionControl: true,
    });

    // Base tiles (OpenStreetMap)
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '© OpenStreetMap contributors',
    }).addTo(this.map);

    // Safe default view (Vienna)
    this.map.setView([48.2082, 16.3738], 12);
  }

  setCenter(lat: number, lng: number, zoom = 13): void {
    this.map?.setView([lat, lng], zoom);
  }

  setMarker(lat: number, lng: number): void {
    if (!this.map) return;

    if (this.markerLayer) {
      this.map.removeLayer(this.markerLayer);
    }
    this.markerLayer = L.marker([lat, lng]).addTo(this.map);
  }

  displayRoute(geoJson: GeoJSON.GeoJsonObject | null): void {
    if (!this.map) return;

    if (this.routeLayer) {
      this.map.removeLayer(this.routeLayer);
      this.routeLayer = null;
    }

    if (!geoJson) return;

    this.routeLayer = L.geoJSON(geoJson, {
      style: { color: '#3388ff', weight: 4 },
    }).addTo(this.map);

    const bounds = this.routeLayer.getBounds();
    if (bounds.isValid()) {
      this.map.fitBounds(bounds, { padding: [20, 20] });
    }
  }

  clear(): void {
    if (this.routeLayer && this.map) {
      this.map.removeLayer(this.routeLayer);
      this.routeLayer = null;
    }
    if (this.markerLayer && this.map) {
      this.map.removeLayer(this.markerLayer);
      this.markerLayer = null;
    }
  }

  destroy(): void {
    this.map?.remove();
    this.map = null;
    this.routeLayer = null;
    this.markerLayer = null;
  }
}