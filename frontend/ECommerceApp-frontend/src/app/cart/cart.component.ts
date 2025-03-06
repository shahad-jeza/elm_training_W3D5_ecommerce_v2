import { Component } from '@angular/core';
import { CartService } from '../services/cart.service';
import { Product } from '../services/product.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart',
  standalone:true,
  imports: [CommonModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  cartItems: Product[] = [];

  constructor(private cartService: CartService) {
    this.cartItems = this.cartService.getCartItems();
  }

  // Remove an item from the cart
  removeFromCart(productId: number): void {
    this.cartService.removeFromCart(productId);
    this.cartItems = this.cartService.getCartItems(); // Refresh the cart items
  }

  // Clear the cart
  clearCart(): void {
    this.cartService.clearCart();
    this.cartItems = []; // Refresh the cart items
  }
}