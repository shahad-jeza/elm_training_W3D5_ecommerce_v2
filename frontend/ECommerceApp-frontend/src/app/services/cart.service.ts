import { Injectable } from '@angular/core';
import { Product } from './product.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems: Product[] = [];

  constructor() {}

  // Add a product to the cart
  addToCart(product: Product): void {
    this.cartItems.push(product);
    console.log('Product added to cart:', product); // Debug log
  }

  // Get all items in the cart
  getCartItems(): Product[] {
    return this.cartItems;
  }

  // Remove an item from the cart by ID
  removeFromCart(productId: number): void {
    this.cartItems = this.cartItems.filter(item => item.id !== productId);
    console.log('Product removed from cart:', productId); // Debug log
  }

  // Clear the cart
  clearCart(): void {
    this.cartItems = [];
    console.log('Cart cleared'); // Debug log
  }
}