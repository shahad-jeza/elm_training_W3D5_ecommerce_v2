import { Component, OnInit } from '@angular/core';
import { ProductService, Product } from '../services/product.service';
import { CommonModule } from '@angular/common'; 
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule], 
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  constructor(private productService: ProductService) {}

  errorMessage: string = '';

  ngOnInit(): void {
    this.productService.getProducts().subscribe({
      next: data => this.products = data,
      error: error => this.errorMessage = 'Failed to load products'
    });
  }
}