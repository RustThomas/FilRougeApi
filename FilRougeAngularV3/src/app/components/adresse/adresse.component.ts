import { Component, OnDestroy, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { FormsModule, FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';

import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import { Adresse } from '../../Models/adresse';
import { MainService } from '../../services/main.service';
import { Router } from '@angular/router';


//Import pour formulaire


@Component({
  selector: 'app-adresse',
  standalone: true,
  imports: [AsyncPipe, FormsModule,ReactiveFormsModule, NgForOf,NgIf],
  templateUrl: './adresse.component.html',
  styleUrl: './adresse.component.css'
})

export class AdresseComponent implements OnDestroy, OnInit {

  hasError: boolean = false

  //adresse subscription permet récupère permet d'observer l'observable, il modifie la valeurs de adresses à chaque fois que la base de donnée est modifiée
	private adresseSubscription!: Subscription
  adresses : Adresse[] = [];
  ids : number[]= [];

  idEdit !: number;
  //on récupère l'obserble pour le mettre dans la page de donnée
  adresses$ : Observable<Adresse[]> = this.adresseService.getAdressesList();
  
  adresseForm!: FormGroup;

  constructor(private adresseService: MainService, private router : Router) {
  }

  ngOnInit(): void {
    this.adresseSubscription = this.adresseService.getAdressesList().subscribe(v => 
      {
        this.adresses = v
        this.adresses.forEach(a => {
          this.ids.push(a.id)
        });
      }
    )
   
    this.adresseForm= new FormGroup({
        appartement: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        rue: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        ville: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        codePostal: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        pays: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
      });
  }


  
  ngOnDestroy(): void{
		if(this.adresseSubscription)
			this.adresseSubscription.unsubscribe()
	}

  get appartement() {
		return this.adresseForm.get('appartement')
	}
	get rue(){
		return this.adresseForm.get('rue')
	}
  get ville(){
		return this.adresseForm.get('ville')
	}
  get codePostal(){
		return this.adresseForm.get('codePostal')
	}
  get pays(){
		return this.adresseForm.get('pays')
	}

  getAdresseFormulaire() : Adresse{
    const adresse: Adresse = this.adresseForm.value as Adresse;
    return adresse
  }

  idExists(id: number): boolean {
    return this.ids.includes(id);
  }


  addAdresse(){
      
      let a = this.getAdresseFormulaire();
      a.id = 0;
      //alert(`L'appartement est ${a.appartement}`)
      
      this.adresseSubscription = this.adresseService.createAdress(a).subscribe(
        {
          next: (data) => { this.router.navigateByUrl('/adresses') },
          error: (err) => { this.hasError = true }
        }
      )      
  }

  

  deleteAdresse(id: number){
    
		this.adresseSubscription = this.adresseService.deleteAdress(id).subscribe({
			next: (data) => {  },
			error: (err) => { this.hasError = true }
		})
  
	}
  
  editAdresse(id : number){
    alert(id);
    //alert(this.ids);
    //alert(this.idExists(id)); idExist ne marche pas revoir
    let adresse = this.getAdresseFormulaire();
    adresse.id = id;
		this.adresseSubscription = this.adresseService.editAdress(id, adresse).subscribe({
			next: (data) => { this.router.navigateByUrl('/adresses') },
			error: (err) => { this.hasError = true }
		})
	}

   
    /*
  editAdresse(adresse: any){
		this.adresseSubscription = this.adresseService.editAdress(adresse).subscribe({
			next: (data) => { this.router.navigateByUrl('/domains') },
			error: (err) => { this.hasError = true }
		})
	}
  */
}



