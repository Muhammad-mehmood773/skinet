import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Brands } from '../shared/models/brand';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { ShopParams } from '../shared/models/shopParams';
import { Type } from '../shared/models/type';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  BaseUrl = "https://localhost:5001/api/"

  constructor(private http: HttpClient) { }

  getProduct(shopParams: ShopParams){

    let params = new HttpParams();

    if (shopParams.brandId > 0) params = params.append('brandId', shopParams.brandId);
    if (shopParams.typeId) params = params.append('typeId', shopParams.typeId);
     params = params.append('sort', shopParams.sort);
     params = params.append('pageIndex', shopParams.pageNumber);
     params = params.append('pageSize', shopParams.pageSize);
     if(shopParams.search) params = params.append('search', shopParams.search);

 
    return this.http.get<Pagination<Product[]>>(this.BaseUrl + "products" ,{params});
  }

  getProducts(id: number){
    return this.http.get<Product>(this.BaseUrl + "products/" + id);
  }

  getBrands(){
    return this.http.get<Brands[]>(this.BaseUrl + "products/brands");
  }

  getTypes(){
    return this.http.get<Type[]>(this.BaseUrl + "products/types");
  }
}
