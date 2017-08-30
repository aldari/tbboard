import { Injectable } from '@angular/core';
import { Headers, Http, Response, RequestOptions, URLSearchParams  } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

import { Category } from './category.model';
import { Quote } from './quote.model';

@Injectable()
export class QuoteService {
    private url = 'http://localhost:58828/api/quote';

    constructor(private http: Http) {}

    getQuotes(): Observable<Quote[]> {
        return this.http.get(this.url)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || 'Server error'));
    }

    getQuotes2(authorName?: string, categoryId?: string): Observable<Quote[]> {
        const myParams  = new URLSearchParams();
        if (authorName) {
            myParams.append('author', authorName);
        }
        if (categoryId) {
            myParams.append('catid', categoryId);
        }
        return this.http.get(this.url, { params: myParams })
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || 'Server error'));
    }

    get(id: string) {
        return this.http.get(this.url + '/' + encodeURIComponent(id))
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || 'Server error'));
    }

    add(quote: Quote) {
        const headers = new Headers({'Content-Type': 'application/json'});
        return this.http.post(this.url, JSON.stringify(quote), {headers: headers});
    }

    change(quote: Quote) {
        const headers = new Headers({'Content-Type': 'application/json'});
        return this.http.put(this.url + '/' + encodeURIComponent(quote.id), JSON.stringify(quote), {headers: headers});
    }

    delete(id: string) {
        const url = this.url + '/' + encodeURIComponent(id);
        return this.http.delete(url)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}
