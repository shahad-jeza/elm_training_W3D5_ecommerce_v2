import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService, Product } from '../services/product.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'] 
})
export class ProductDetailComponent implements OnInit {
  product: Product | undefined;
  errorMessage: string | undefined; // Add an error message property

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService
  ) {}


  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    console.log('Fetching product with ID:', id); // Debug log
  
    this.productService.getProductById(id).subscribe({
      next: (data) => {
        console.log('Product data received:', data); // Debug log
        this.product = data;
        console.log('Product object after assignment:', this.product); // Debug log
      },
      error: (err) => {
        console.error('Error fetching product:', err); // Debug log
        this.errorMessage = 'Failed to load product details. Please try again later.';
      }
    });

    console.log('ProductDetailComponent initialized!');
  }
  }

