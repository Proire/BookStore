﻿using BookStoreRL.CustomExceptions.BookStoreRL.Exceptions;
using BookStoreRL.Entity;
using BookStoreRL.Interfaces.CardRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRL.Services.CardRepository
{
    public class CartCommandRepository : ICartCommandRepository
    {
        private readonly UserDbContext _context;

        public CartCommandRepository(UserDbContext context)
        {
            _context = context;
        }
        public async Task AddBooktoCartAsync(int userId, int bookId, int quantity)
        {
            try
            {
                // Find the user's cart
                var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
                if (cart == null)
                {
                    // Create a new cart for the user if it doesn't exist
                    cart = new Cart
                    {
                        UserId = userId,
                        CartItems = new List<CartItem>()
                    };
                    _context.Carts.Add(cart);
                }

                // Find the book in the database
                var book = await _context.Books.FindAsync(bookId);
                if (book == null)
                {
                    throw new CartException("Book not found.");
                }

                // Check if the book is already in the cart
                var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
                if (existingCartItem != null)
                {
                    // If the book is already in the cart, update the quantity
                    if (book.StockQuantity >= existingCartItem.QuantityToPurchase + quantity)
                    {
                        existingCartItem.QuantityToPurchase += quantity;
                    }
                    else
                    {
                        throw new CartException("Not enough stock available.");
                    }
                }
                else
                {
                    // If the book is not in the cart, add a new cart item
                    if (book.StockQuantity >= quantity)
                    {
                        var cartItem = new CartItem
                        {
                            CartId = cart.Id,
                            BookId = bookId,
                            QuantityToPurchase = quantity
                        };
                        cart.CartItems.Add(cartItem);
                    }
                    else
                    {
                        throw new CartException("Not enough stock available.");
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                throw new CartException("An error occurred while updating the cart.", ex);
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                throw new CartException("An unexpected error occurred.", ex);
            }
        }
    }
}