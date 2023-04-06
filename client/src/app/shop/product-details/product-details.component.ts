import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
product?: Product;
quantity = 1;
quantityToBasket = 0;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute,
     private bcService: BreadcrumbService, private basketService: BasketService){
      this.bcService.set('@productDetails', ' ')
     }

  ngOnInit(): void {
    this.LoadProduct();

  }
  LoadProduct(){
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) this.shopService.getProducts(+id).subscribe({
      next: product => 
      {
        this.product = product;
        this.bcService.set('@productDetails', product.name);
        this.basketService.basketSource$.pipe(take(1)).subscribe({
          next: basket => {
            const item = basket?.items.find(x => x.id === +id);
            if (item) { 
            this.quantity = item.quantity;
            this.quantityToBasket = item.quantity;
            }
          }
        })
      
      },
      error: error => console.log(error)
    })
  }

  incrementQuantity() {
    this.quantity++;
  }

  decrementQuantity() {
    this.quantity--;
  }

  updateBasket() {
    if (this.product)
    {
      if (this.quantity > this.quantityToBasket){
        const itemsToadd = this.quantity - this.quantityToBasket;
        this.quantityToBasket += itemsToadd;
        this.basketService.addItemToBasket(this.product,itemsToadd);
      }else{
        const itemsToRemove = this.quantityToBasket - this.quantity;
        this.quantityToBasket -= itemsToRemove;
        this.basketService.removeItemFromBasket(this.product.id, itemsToRemove);
      }
    }
  }

  get buttonText() {
    return this.quantityToBasket === 0 ? 'Add to basket' : 'Update basket';
  }
}
