import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../api.config';
import { Item } from '../models/item.model';

@Injectable({ providedIn: 'root' })
export class ItemService {
    private readonly http = inject(HttpClient);

    getAll(): Observable<Item[]> {
        return this.http.get<Item[]>(`${API_URL}/Item`);
    }
}