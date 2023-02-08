import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit{
  product?: Product;
  itemId?: number;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute){}

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct(){
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) this.shopService.getProduct(+id).subscribe({
      next: response => this.product = response,
      error: error => console.log(error)
    })
  }

}
