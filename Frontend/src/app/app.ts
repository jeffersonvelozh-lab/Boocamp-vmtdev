import { Component, signal } from '@angular/core';
import { MatDivider } from '@angular/material/divider';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './shared/navbar-component/navbar-component';
import { FooterComponent } from './shared/footer-component/footer-component';
import { HeaderComponent } from './shared/header-component/header-component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MatDivider, NavbarComponent, FooterComponent, HeaderComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('Frontend');
}
