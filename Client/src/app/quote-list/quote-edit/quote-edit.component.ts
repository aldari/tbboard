import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

import { ValidateService } from './../../validate.service';
import { QuoteService } from './../quote.service';
import { CategoryService } from './../category.service';
import { Category } from './../category.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Quote } from './../quote.model';

@Component({
  selector: 'app-quote-edit',
  templateUrl: './quote-edit.component.html',
  styleUrls: ['./quote-edit.component.css']
})
export class QuoteEditComponent implements OnInit {
  @ViewChild('f') createForm: NgForm;
  item: Quote = new Quote();
  private categories: Category[];

  constructor(private categoryService: CategoryService,
    private quoteService: QuoteService,
    private validateService: ValidateService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.getCategories();
    this.loadQuoteById();
  }

  loadQuoteById() {
    const id = this.route.snapshot.params['id'];
    this.quoteService.get(id).subscribe(
      quote => {
        this.item = quote;
        console.log(this.item);
      },
      err => {
           console.log(err);
       }
    );
  }

  onSubmit(form: NgForm) {
    const quote = new Quote();
    quote.text =  this.createForm.value.text;
    quote.author = this.createForm.value.author;
    quote.categoryId = this.createForm.value.category;
    quote.id = this.route.snapshot.params['id'];

    if (this.createForm.valid) {
      // save data
      this.quoteService.change(quote).subscribe(
        (response) => {
            this.router.navigate(['/quotes']);
        },
        (error) => console.log(error)
      );
    } else {
        this.validateService.validateAllFields(this.createForm.form);
    }
  }

  getCategories() {
    this.categoryService.getCategories().subscribe(
      categories => this.categories = categories,
      err => {
           console.log(err);
       }
    );
  }
}
