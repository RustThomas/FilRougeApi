import { Component, OnDestroy, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { FormsModule, FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';

import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import { Auteur } from '../../Models/auteur';
import { MainService } from '../../services/main.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auteur',
  standalone: true,
  imports: [AsyncPipe, FormsModule,ReactiveFormsModule, NgForOf,NgIf],
  templateUrl: './auteur.component.html',
  styleUrl: './auteur.component.css'
})
export class AuteurComponent {

  hasError: boolean = false

  //adresse subscription permet récupère permet d'observer l'observable, il modifie la valeurs de adresses à chaque fois que la base de donnée est modifiée
	private auteurSubscription!: Subscription
  auteurs : Auteur[] = [];
  auteurIds : number[]= [];

  idEdit !: number;
  //on récupère l'obserble pour le mettre dans la page de donnée
  Auteurs$ : Observable<Auteur[]> = this.mainService.getAuteurList();
  
  auteurForm!: FormGroup;

  constructor(private mainService: MainService, private router : Router) {
  }

  ngOnInit(): void {
    this.auteurSubscription = this.mainService.getAuteurList().subscribe(v => 
      {
        this.auteurs = v
        this.auteurs.forEach(a => {
          this.auteurIds.push(a.id)
        });
      }
    )
   
    this.auteurForm= new FormGroup({
        nom : new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        prenom: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        email: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        telephone: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        grade: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        fullname: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
      });
  }
  
  ngOnDestroy(): void{
		if(this.auteurSubscription)
			this.auteurSubscription.unsubscribe()
	}

  get nom() {
		return this.auteurForm.get('nom')
	}
	get prenom(){
		return this.auteurForm.get('prenom')
	}
  get email() {
		return this.auteurForm.get('email')
	}
	get telephone(){
		return this.auteurForm.get('telephone')
	} 
  get grade() {
		return this.auteurForm.get('grade')
	}
	get fullname(){
		return this.auteurForm.get('fullname')
	}

  getAuteurFromFormulaire() : Auteur{
    const Auteur: Auteur = this.auteurForm.value as Auteur;
    return Auteur
  }

  idExists(id: number): boolean {
    return this.auteurIds.includes(id);
  }


  addAuteur(){
      let a = this.getAuteurFromFormulaire();
      a.id = 0;
      this.auteurSubscription = this.mainService.createAuteur(a).subscribe(
        {
          next: (data) => { this.router.navigateByUrl('/auteurs') },
          error: (err) => { this.hasError = true }
        }
      )      
  }

  

  deleteAuteur(id: number){
    
		this.auteurSubscription = this.mainService.deleteAuteur(id).subscribe({
			next: (data) => {  },
			error: (err) => { this.hasError = true }
		})
  
	}
  
  editAuteur(id : number){
    //alert(id);
    //alert(this.ids);
    //alert(this.idExists(id)); idExist ne marche pas revoir
    let Auteur = this.getAuteurFromFormulaire();
    Auteur.id = id;
		this.auteurSubscription = this.mainService.editAuteur(id, Auteur).subscribe({
			next: (data) => { this.router.navigateByUrl('/auteurs') },
			error: (err) => { this.hasError = true }
		})
	}

}
