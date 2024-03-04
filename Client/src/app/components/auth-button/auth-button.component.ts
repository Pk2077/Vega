import { DOCUMENT } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'auth-button',
  templateUrl: './auth-button.component.html',
  styleUrl: './auth-button.component.css',
})
export class AuthButtonComponent {
  // Inject the authentication service into your component through the constructor
  constructor(
    @Inject(DOCUMENT) public document: Document,
    public auth: AuthService
  ) {}
}
