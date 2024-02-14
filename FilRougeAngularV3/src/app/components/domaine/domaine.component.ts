import { Component, OnDestroy, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { FormsModule, FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';

import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import { Domaine } from '../../Models/domaine';
import { MainService } from '../../services/main.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-domaine',
  standalone: true,
  imports: [AsyncPipe, FormsModule,ReactiveFormsModule, NgForOf,NgIf],
  templateUrl: './domaine.component.html',
  styleUrl: './domaine.component.css'
})
export class DomaineComponent {
  hasError: boolean = false

  //adresse subscription permet récupère permet d'observer l'observable, il modifie la valeurs de adresses à chaque fois que la base de donnée est modifiée
	private domaineSubscription!: Subscription
  domaines : Domaine[] = [];
  domaineIds : number[]= [];

  idEdit !: number;
  //on récupère l'obserble pour le mettre dans la page de donnée
  domaines$ : Observable<Domaine[]> = this.mainService.getDomaineList();
  
  domaineForm!: FormGroup;

  constructor(private mainService: MainService, private router : Router) {
  }

  ngOnInit(): void {
    this.domaineSubscription = this.mainService.getDomaineList().subscribe(v => 
      {
        this.domaines = v
        this.domaines.forEach(a => {
          this.domaineIds.push(a.id)
        });
      }
    )
   
    this.domaineForm= new FormGroup({
        nom : new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        description: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
      });
  }


  
  ngOnDestroy(): void{
		if(this.domaineSubscription)
			this.domaineSubscription.unsubscribe()
	}

  get nom() {
		return this.domaineForm.get('nom')
	}
	get description(){
		return this.domaineForm.get('description')
	}


  getDomaineFromFormulaire() : Domaine{
    const domaine: Domaine = this.domaineForm.value as Domaine;
    return domaine
  }

  idExists(id: number): boolean {
    return this.domaineIds.includes(id);
  }


  addDomaine(){
      
      let a = this.getDomaineFromFormulaire();
      a.id = 0;
      //alert(`L'appartement est ${a.appartement}`)
      
      this.domaineSubscription = this.mainService.createDomaine(a).subscribe(
        {
          next: (data) => { this.router.navigateByUrl('/domaines') },
          error: (err) => { this.hasError = true }
        }
      )      
  }

  

  deleteDomaine(id: number){
    
		this.domaineSubscription = this.mainService.deleteDomaine(id).subscribe({
			next: (data) => {  },
			error: (err) => { this.hasError = true }
		})
  
	}
  
  editDomaine(id : number){
    //alert(id);
    //alert(this.ids);
    //alert(this.idExists(id)); idExist ne marche pas revoir
    let domaine = this.getDomaineFromFormulaire();
    domaine.id = id;
		this.domaineSubscription = this.mainService.editDomaine(id, domaine).subscribe({
			next: (data) => { this.router.navigateByUrl('/domaines') },
			error: (err) => { this.hasError = true }
		})
	}


}
