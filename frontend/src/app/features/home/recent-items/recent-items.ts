import { Component, computed, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { CONDITION_LABELS } from '../../../core/models/item.model';
import { ItemService } from '../../../core/services/item.service';

@Component({
  selector: 'app-recent-items',
  imports: [],
  templateUrl: './recent-items.html',
  styleUrl: './recent-items.css',
})
export class RecentItems {
  private readonly itemService = inject(ItemService);
  private readonly allItems = toSignal(this.itemService.getAll(), { initialValue: [] });

  items = computed(() => this.allItems().slice(0, 8));
  labels = CONDITION_LABELS;
}
