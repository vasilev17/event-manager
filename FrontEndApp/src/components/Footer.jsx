import React from "react";

export default function Footer() {
  return (
    <footer class="bg-purple-950 text-white bottom-0 w-screen p-24 pr-28">
      <div className="">
        <div className="footer-semibold">
          За нас
        </div>
        <div className="footer-bold">
          Ние сме водеща платформа за организиране и откриване на събития – от
          концерти и фестивали до бизнес конференции и уъркшопи. Помагаме на
          хората да се свързват и да създават незабравими моменти.
          <br />
        </div>
        <div className="footer-semibold">
          <br />
          Контакти
        </div>
        <div className="footer-bold">
          Адрес: ул. „Примерна“ 123, София
          <br />
        </div>
        <div className="footer-bold">
          Телефон: +359 123 456 789
          <br />
        </div>
        <div className="footer-bold">
          Имейл: info@example.com
          <br />
        </div>
        <div className="footer-semibold">
          <br />
          Социални мрежи
        </div>
        <div className="footer-bold">
          Последвайте ни в социалните мрежи, за да сте винаги в крак с
          най-новите събития:
        </div>
        <div className="footer-semibold">
          <a>Facebook |</a>
          <a> Instagram |</a>
          <a> Twitter |</a>
          <a> LinkedIn</a>
        </div>
      </div>
    </footer>
  );
}
