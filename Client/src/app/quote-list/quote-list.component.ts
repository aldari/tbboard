import { Component, OnInit } from '@angular/core';

import { Category } from './category.model';
import { Quote } from './quote.model';
import { QuoteService } from './quote.service';
import { CategoryService } from './category.service';

@Component({
  selector: 'app-quote-list',
  templateUrl: './quote-list.component.html',
  styleUrls: ['./quote-list.component.css']
})
export class QuoteListComponent implements OnInit {
  private quotes: Quote[];
  private categories: Category[];
  private selectedCategory: Category = new Category('', '');
  private filterAuthor: string;

  constructor(private quoteService: QuoteService, private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.getQuotes();
    this.getCategories();
  }

  getQuotes(): void {
    this.quoteService.getQuotes().subscribe(
      quotes => this.quotes = quotes,
      err => {
           console.log(err);
       }
    );
  }

  getCategories() {
    this.categoryService.getCategories().subscribe(
      categories => this.categories = categories,
      err => {
           console.log(err);
       }
    );
  }

  onDelete(id: string, i: number) {
    this.quoteService.delete(id).subscribe(
      () => {this.quotes.splice(i, 1);},
      err => {
           console.log(err);
       }
    );
  }

  onFilter(authorFilter: string) {
    this.quoteService.getQuotes2(authorFilter, this.selectedCategory.id).subscribe(
      quotes => this.quotes = quotes,
      err => {
           console.log(err);
       }
    );
  }
}
