import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { Product } from '../Product';

@Component({
  selector: 'my-app',
  templateUrl: `./app.component.html`,
})

export class AppComponent  {

    products: any;
    name = 'Angular'; 

    constructor(private http: Http) { }

    GetProducts() {
        this.http.get('/Home/ProductList')
            .subscribe(data => {
                this.products = data.json()
            });
    }
}