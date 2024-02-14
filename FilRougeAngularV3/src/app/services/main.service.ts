import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
//RX
import {Observable} from "rxjs";
//CFG
import { BASE_API_URL } from '../constants/injection';


import { Admin } from "../Models/admin";
import { Adresse } from "../Models/adresse";
import { Auteur} from "../Models/auteur";
import { Domaine } from "../Models/domaine";
import { Emprunt } from "../Models/emprunt";
import { Lecteur } from "../Models/lecteur";
import { Livre } from "../Models/livre";

@Injectable({
  providedIn: 'root'
})
export class MainService {

  constructor(private http: HttpClient, @Inject(BASE_API_URL) private baseUrl: string) { 

  }

  getAdminList(): Observable<Admin[]> { return this.http.get<Admin[]>(`${this.baseUrl}/admins`) }
	getAdmin(id: number): Observable<Admin> { return this.http.get<Admin>(`${this.baseUrl}/admins/${id}`) }
  createAdmin(admin: Admin): Observable<Admin> {return this.http.post<Admin>(`${this.baseUrl}/admins`, admin)}
  editAdmin(id:number, admin: Admin): Observable<Admin> { return this.http.put<Admin>(`${this.baseUrl}/admins/${id}`, admin) }
	deleteAdmin(id: number): Observable<boolean> { return this.http.delete<boolean>(`${this.baseUrl}/admins/${id}`) }
  

  getAdressesList(): Observable<Adresse[]> { return this.http.get<Adresse[]>(`${this.baseUrl}/adresses`) }
	getAdress(id: number): Observable<Adresse> { return this.http.get<Adresse>(`${this.baseUrl}/adresses/${id}`) }
  createAdress(adresse: Adresse): Observable<Adresse> {return this.http.post<Adresse>(`${this.baseUrl}/adresses`, adresse)}
  editAdress(id:number, adresse: Adresse): Observable<Adresse> { return this.http.put<Adresse>(`${this.baseUrl}/adresses/${id}`, adresse) }
	deleteAdress(id: number): Observable<boolean> { return this.http.delete<boolean>(`${this.baseUrl}/adresses/${id}`) }
  
  getAuteurList(): Observable<Auteur[]> { return this.http.get<Auteur[]>(`${this.baseUrl}/auteurs`) }
	getAuteur(id: number): Observable<Auteur> { return this.http.get<Auteur>(`${this.baseUrl}/auteurs/${id}`) }
  createAuteur(auteur: Auteur): Observable<Auteur> {return this.http.post<Auteur>(`${this.baseUrl}/auteurs`, auteur)}
  editAuteur(id:number, auteur: Auteur): Observable<Auteur> { return this.http.put<Auteur>(`${this.baseUrl}/auteurs/${id}`, auteur) }
	deleteAuteur(id: number): Observable<boolean> { return this.http.delete<boolean>(`${this.baseUrl}/auteurs/${id}`) }
  
  getDomaineList(): Observable<Domaine[]> { return this.http.get<Domaine[]>(`${this.baseUrl}/Domaines`) }
	getDomaine(id: number): Observable<Domaine> { return this.http.get<Domaine>(`${this.baseUrl}/Domaines/${id}`) }
  createDomaine(domaine: Domaine): Observable<Domaine> {return this.http.post<Domaine>(`${this.baseUrl}/Domaines`, domaine)}
  editDomaine(id:number, domaine: Domaine): Observable<Domaine> { return this.http.put<Domaine>(`${this.baseUrl}/Domaines/${id}`, domaine) }
	deleteDomaine(id: number): Observable<boolean> { return this.http.delete<boolean>(`${this.baseUrl}/Domaines/${id}`) }
  
  
  getEmpruntList(): Observable<Emprunt[]> { return this.http.get<Emprunt[]>(`${this.baseUrl}/Emprunts`) }
	getEmprunt(id: number): Observable<Emprunt> { return this.http.get<Emprunt>(`${this.baseUrl}/Emprunts/${id}`) }
  createEmprunt(Emprunt: Emprunt): Observable<Emprunt> {return this.http.post<Emprunt>(`${this.baseUrl}/Emprunts`, Emprunt)}
  editEmprunt(id:number, Emprunt: Emprunt): Observable<Emprunt> { return this.http.put<Emprunt>(`${this.baseUrl}/Emprunts/${id}`, Emprunt) }
	deleteEmprunt(id: number): Observable<boolean> { return this.http.delete<boolean>(`${this.baseUrl}/Emprunts/${id}`) }
  
  getLecteurList(): Observable<Lecteur[]> { return this.http.get<Lecteur[]>(`${this.baseUrl}/Lecteurs`) }
	getLecteur(id: number): Observable<Lecteur> { return this.http.get<Lecteur>(`${this.baseUrl}/Lecteurs/${id}`) }
  createLecteur(lecteur: Lecteur): Observable<Lecteur> {return this.http.post<Lecteur>(`${this.baseUrl}/Lecteurs`, lecteur)}
  editLecteur(id:number, lecteur: Lecteur): Observable<Lecteur> { return this.http.put<Lecteur>(`${this.baseUrl}/Lecteurs/${id}`, lecteur) }
	deleteLecteur(id: number): Observable<boolean> { return this.http.delete<boolean>(`${this.baseUrl}/Lecteurs/${id}`) }
  
  getLivreList(): Observable<Livre[]> { return this.http.get<Livre[]>(`${this.baseUrl}/Livres`) }
	getLivre(id: number): Observable<Livre> { return this.http.get<Livre>(`${this.baseUrl}/Livres/${id}`) }
  createLivre(livre: Livre): Observable<Livre> {return this.http.post<Livre>(`${this.baseUrl}/Livres`, livre)}
  editLivre(id:number, livre: Livre): Observable<Livre> { return this.http.put<Livre>(`${this.baseUrl}/Livres/${id}`, livre) }
	deleteLivre(id: number): Observable<boolean> { return this.http.delete<boolean>(`${this.baseUrl}/Livres/${id}`) }
  
  

}

/*
  getAllAdresses():Adresse[] {
    return this.adressesData;
  }
  */

	
  /*
  getDomainList(): Observable<Domain[]> { return this.http.get<Domain[]>(`${this.baseUrl}/domains`) }
	getDomain(id: number): Observable<Domain> { return this.http.get<Domain>(`${this.baseUrl}/domains/${id}`) }
	createDomain(domain: Domain): Observable<Domain> { return this.http.post<Domain>(`${this.baseUrl}/domains`, domain) }
	editDomain(domain: Domain): Observable<Domain> { return this.http.put<Domain>(`${this.baseUrl}/domains`, domain) }
	deleteDomain(id: number): Observable<boolean> { return this.http.delete<boolean>(`${this.baseUrl}/domains/${id}`) }
  */

/*
  adminsData : Admin[]= [{
    id: 0,
    nom: "string",
    prenom: "string",
    email: "string",
    telephone: "string",
    motDePasse: "string"
  }]

  adressesData : Adresse[]= [{
    id: 0,
    appartement: "5",
    rue: "string",
    ville: "string",
    codePostal: "string",
    pays: "string"
  },
  {
    id: 1,
    appartement: "6",
    rue: "string",
    ville: "string",
    codePostal: "string",
    pays: "string"
  }]

  auteursData : Auteur[]= [{
    id: 0,
    nom: "string",
    prenom: "string",
    email: "string",
    telephone: "string",
    grade: "string",
    fullName: "string"
  }]


  domainesData : Domaine[]= [{
    id: 0,
    nom: "string",
    description: "string"
  }]

  LecteursData : Emprunt[]= [{
    id: 0,
    dateEmprunt: new Date(2024),
    dateRetour: new Date(2023),
    lecteurId: 0,
    livreId: 0
  }]

  lecteursData : Lecteur[]= [{
    id: 0,
    nom: "string",
    prenom: "string",
    email: "string",
    telephone: "string",
    motDePasse: "string",
    adresseId: 0
  }]

  livresData : Livre[]= [{
    id: 0,
    titre: "string",
    nombrePages: 0,
    statutDuLivre: 0,
    etatDuLivre: 0,
    auteurId: 0,
    domaineId: 0
  }]
  */