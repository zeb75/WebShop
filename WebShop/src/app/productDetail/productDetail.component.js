"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core"); /* OnInit to run on page */ /* Input to send from one komp to another*/
var http_1 = require("@angular/http");
var Product_1 = require("../Product");
var ProductDetailComponent = /** @class */ (function () {
    function ProductDetailComponent(http) {
        this.http = http;
    }
    ProductDetailComponent.prototype.ngOnInit = function () {
    };
    ProductDetailComponent.prototype.EditPerson = function () {
        console.log(this.productDetail);
        this.http.post('/Home/ProductDetail', this.productDetail)
            .subscribe(function (data) {
            console.log(data);
            if (data.status == 200) {
            }
        });
    };
    ProductDetailComponent.prototype.AddToCart = function () {
        this.http.post('/Home/AddToCart', this.productDetail)
            .subscribe(function (data) {
            console.log(data);
            if (data.status == 200) {
            }
        });
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Product_1.Product)
    ], ProductDetailComponent.prototype, "productDetail", void 0);
    ProductDetailComponent = __decorate([
        core_1.Component({
            selector: 'my-product-detail',
            templateUrl: "./productDetail.component.html",
        }),
        __metadata("design:paramtypes", [http_1.Http])
    ], ProductDetailComponent);
    return ProductDetailComponent;
}());
exports.ProductDetailComponent = ProductDetailComponent;
//# sourceMappingURL=productDetail.component.js.map