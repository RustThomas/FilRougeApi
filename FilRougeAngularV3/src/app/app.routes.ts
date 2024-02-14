import { Routes } from '@angular/router';

import {AdminComponent} from "./components/admin/admin.component";
import {AdresseComponent} from "./components/adresse/adresse.component";
import {AuteurComponent} from "./components/auteur/auteur.component";
import {DomaineComponent} from "./components/domaine/domaine.component";
import {EmpruntComponent} from "./components/emprunt/emprunt.component";
import {LecteurComponent} from "./components/lecteur/lecteur.component";
import {LivreComponent} from "./components/livre/livre.component";


export const routes: Routes = [
    {path: '', component: AdminComponent },
    {path: 'admins', component: AdminComponent },
    {path: 'adresses', component: AdresseComponent },
    {path: 'auteurs', component: AuteurComponent},
    {path: 'domaines', component: DomaineComponent},
    {path: 'emprunts', component: EmpruntComponent},
    {path: 'lecteurs', component: LecteurComponent},
    {path: 'livres', component: LivreComponent},
    {path: '**', redirectTo: '/'}

];

