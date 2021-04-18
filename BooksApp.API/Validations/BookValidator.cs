using BooksApp.API.Models;
using BooksApp.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApp.API.Validations
{
    public class BookValidator:AbstractValidator<Book>
    {
        public BookValidator(BookServices service)
        {
            RuleFor(b => b.Title).NotEmpty().WithMessage("Debe insertar el titulo del libro");
            RuleFor(b => b.PageCount).GreaterThan(0).WithMessage("Inserte la cantidad de paginas del libro");
        }
    }
}
