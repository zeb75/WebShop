﻿import { Component, OnInit, Input } from '@angular/core'; /* OnInit to run on page *//* Input to send from one komp to another*/
import { Http } from '@angular/http';
import { Product } from '../Product';

@Component({
    selector: 'my-product-detail',
    templateUrl: `./productDetail.component.html`,
})
export class ProductDetailComponent implements OnInit {
    @Input() productDetail: Product;

    constructor(private http: Http) { }

    ngOnInit(): void {

    }

    EditPerson()
    {
        console.log(this.productDetail);
        this.http.post('/Home/ProductDetail', this.productDetail)
            .subscribe(data => {
                console.log(data);
                if (data.status == 200) {

                }
            });
    }

    AddToCart()
    {
        this.http.post('/Home/AddToCart', this.productDetail)
            .subscribe(data =>
            {
                console.log(data);
                if (data.status == 200)
                {

                }
            });
    }

}