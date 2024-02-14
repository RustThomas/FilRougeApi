import { Component, OnDestroy, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { FormsModule, FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';

import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import { Livre } from '../../Models/livre';
import { MainService } from '../../services/main.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-livre',
  standalone: true,
  imports: [AsyncPipe, FormsModule,ReactiveFormsModule, NgForOf,NgIf],
  templateUrl: './livre.component.html',
  styleUrl: './livre.component.css'
})
export class LivreComponent {

  hasError: boolean = false

  //adresse subscription permet récupère permet d'observer l'observable, il modifie la valeurs de adresses à chaque fois que la base de donnée est modifiée
	private livreSubscription!: Subscription
  livres : Livre[] = [];
  livreIds : number[]= [];

  idEdit !: number;
  //on récupère l'obserble pour le mettre dans la page de donnée
  livres$ : Observable<Livre[]> = this.mainService.getLivreList();
  
  livreForm!: FormGroup;

  constructor(private mainService: MainService, private router : Router) {
  }

  ngOnInit(): void {
    this.livreSubscription = this.mainService.getLivreList().subscribe(v => 
      {
        this.livres = v
        this.livres.forEach(a => {
          this.livreIds.push(a.id)
        });
      }
    )
   
    this.livreForm= new FormGroup({
        titre : new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        nombrePages: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        statutDuLivre: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        etatDuLivre: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        auteurId: new FormControl('', [
          Validators.required,
          Validators.minLength(1),
        ]),
        domaineId: new FormControl('', [
          Validators.required,
          Validators.minLength(1),
        ]),
      });
  }
  
  ngOnDestroy(): void{
		if(this.livreSubscription)
			this.livreSubscription.unsubscribe()
	}

	get titre(){
		return this.livreForm.get('titre')
	}
  get nombrePages() {
		return this.livreForm.get('nombrePages')
	}
	get statutDuLivre(){
		return this.livreForm.get('statutDuLivre')
	} 
  get etatDuLivre() {
		return this.livreForm.get('etatDuLivre')
	}
	get auteurId(){
		return this.livreForm.get('auteurId')
	}
  get domaineId(){
		return this.livreForm.get('domaineId')
	}

  getLivreFromFormulaire() : Livre{
    const Livre: Livre = this.livreForm.value as Livre;
    return Livre
  }

  idExists(id: number): boolean {
    return this.livreIds.includes(id);
  }


  addLivre(){
      let a = this.getLivreFromFormulaire();
      a.id = 0;
      this.livreSubscription = this.mainService.createLivre(a).subscribe(
        {
          next: (data) => { this.router.navigateByUrl('/livres') },
          error: (err) => { this.hasError = true }
        }
      )      
  }

  deleteLivre(id: number){
		this.livreSubscription = this.mainService.deleteLivre(id).subscribe({
			next: (data) => {  },
			error: (err) => { this.hasError = true }
		})
  
	}
  
  editLivre(id : number){
    let Livre = this.getLivreFromFormulaire();
    Livre.id = id;
		this.livreSubscription = this.mainService.editLivre(id, Livre).subscribe({
			next: (data) => { this.router.navigateByUrl('/livres') },
			error: (err) => { this.hasError = true }
		})
	}

}
