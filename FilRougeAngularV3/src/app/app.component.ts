import { Component } from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {AdminComponent} from "./components/admin/admin.component";
import {AdresseComponent} from "./components/adresse/adresse.component";
import {AuteurComponent} from "./components/auteur/auteur.component";
import {DomaineComponent} from "./components/domaine/domaine.component";
import {EmpruntComponent} from "./components/emprunt/emprunt.component";
import {LecteurComponent} from "./components/lecteur/lecteur.component";
import {LivreComponent} from "./components/livre/livre.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, AdminComponent,AdresseComponent,AuteurComponent,DomaineComponent,EmpruntComponent,LecteurComponent,LivreComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'FilRougeAngular';
}
