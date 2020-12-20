import { Component, OnInit } from '@angular/core';
import { PoketcgService} from '../../services/poketcg.service';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.css']
})
export class PurchaseComponent implements OnInit {

  // placeholder, to be changed depends on what a user actually sees
  sets:any;

  constructor(public poketcgService:PoketcgService) { }

  ngOnInit(): void {
    this.poketcgService.getPoketcg().subscribe((data) => { this.sets = data; }); 
  }

}
