import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService, Product } from '../services/product.service';
import { CommonModule } from '@angular/common';
import { CartService } from '../services/cart.service';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'] 
})
export class ProductDetailComponent implements OnInit {
  product: Product | undefined;
  errorMessage: string | undefined;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cartService: CartService // Inject CartService
  ) {}

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.productService.getProductById(id).subscribe({
      next: (data) => this.product = data,
      error: (err) => this.errorMessage = 'Failed to load product details.'
    });
  }

  // Add the current product to the cart
  addToCart(product: Product): void {
    this.cartService.addToCart(product);
    alert('Product added to cart!');
  }
}