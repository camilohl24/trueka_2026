import { Component, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { CategoryService } from '../../../core/services/category.service';

@Component({
  selector: 'app-category-list',
  imports: [],
  templateUrl: './category-list.html',
  styleUrl: './category-list.css',
})
export class CategoryList {
  private readonly categoryService = inject(CategoryService);
  categories = toSignal(this.categoryService.getAll(), { initialValue: [] });
}
