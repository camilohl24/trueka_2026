import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../api.config';
import { Category } from '../models/category.models';

@Injectable({ providedIn: 'root' })
export class CategoryService {
    private readonly http = inject(HttpClient);

    getAll(): Observable<Category[]> {
        return this.http.get<Category[]>(`${API_URL}/Category`);
    }
}