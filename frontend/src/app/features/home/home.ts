import { Component } from '@angular/core';
import { CategoryList } from './category-list/category-list';
import { Hero } from './hero/hero';
import { RecentItems } from './recent-items/recent-items';

@Component({
  selector: 'app-home',
  imports: [Hero, CategoryList, RecentItems],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home { }