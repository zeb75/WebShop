import { Component, OnInit} from '@angular/core';
import { Http } from '@angular/http';
import { Product } from './Product';

@Component({
  selector: 'my-app',
  templateUrl: `./app.component.html`,
})

export class AppComponent implements OnInit {

    newProduct: Product = { Id: 0, Brand: "Brand", Model: "Model", Description: "Description", Price: 0, Image: "Image"  }; 
    selectedProduct: Product;
    products: Product[];
    name = 'Angular'; 

    constructor(private http: Http) { }

    ngOnInit(): void {
        this.GetProducts();
    }

    OnSelect(productDetail: Product) {
        if (this.selectedProduct === productDetail) {
            this.selectedProduct = null;
        }
        else {
            this.selectedProduct = productDetail;
        }
    }

    GetProducts() {
        this.http.get('/Home/ProductList')
            .subscribe(data => {
                this.products = data.json()
            });
    }
}