import { NgForm, FormGroup, FormControl } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';

import { Quote } from './../quote.model';
import { Category } from './../category.model';
import { CategoryService } from './../category.service';
import { QuoteService } from './../quote.service';
import { ValidateService } from './../../validate.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-quote-new',
  templateUrl: './quote-new.component.html',
  styleUrls: ['./quote-new.component.css']
})

export class QuoteNewComponent implements OnInit {
  @ViewChild('f') createForm: NgForm;
  private categories: Category[];

  constructor(private categoryService: CategoryService,
    private quoteService: QuoteService,
    private validateService: ValidateService,
    private router: Router) { }

  ngOnInit() {
    this.getCategories();
  }

  onSubmit(form: NgForm) {
    const quote = new Quote();
    quote.author = this.createForm.value.author;
    quote.text = this.createForm.value.text;
    quote.categoryId = this.createForm.value.category;

    if (this.createForm.valid) {
      // save data
      this.quoteService.add(quote).subscribe(
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
