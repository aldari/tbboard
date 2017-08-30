import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { QuoteNewComponent } from './quote-list/quote-new/quote-new.component';
import { QuoteEditComponent } from './quote-list/quote-edit/quote-edit.component';
import { QuoteListComponent } from './quote-list/quote-list.component';

const appRoutes: Routes = [
    {
        path: '',
        redirectTo: '/quotes',
        pathMatch: 'full'
    },
    {
        path: 'quotes',
        component: QuoteListComponent,
        pathMatch: 'full'
    },
    {
        path: 'quotes/add',
        component: QuoteNewComponent
    },
    {
        path: 'quotes/:id',
        component: QuoteEditComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes)],
    exports: [RouterModule]
})

export class AppRoutingModule {
}
