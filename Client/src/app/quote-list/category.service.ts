import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { Category } from './category.model';
import { Quote } from './quote.model';

@Injectable()
export class CategoryService {
    constructor(private http: Http) {}

    getCategories(): Observable<Category[]> {
        return this.http.get('http://localhost:58828/api/category')
        .map((res: Response) => res.json())
        .catch((error: any) => Observable.throw(error.json().error || 'Server error'));
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}