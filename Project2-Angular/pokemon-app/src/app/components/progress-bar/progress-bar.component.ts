import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-progress-bar',
  templateUrl: './progress-bar.component.html',
  styleUrls: ['./progress-bar.component.css']
})
export class ProgressBarComponent implements OnInit {

  @Input() progress!:number;
  @Input() total!:number;
  color!:string;
  constructor() { }

  ngOnInit(): void {
    this.GetProgress();
    
  }
  // not implemented
  GetProgress(){
    if(!this.progress) this.progress = 0; 
    if(this.total === 0) this.total = this.progress;
    if(!this.total) this.total = 100;

    if(this.progress > this.total)
    {
      this.progress = 100;
      this.total = 100;

    }
    this.progress = this.progress/ this.total * 100;
    if(this.progress < 33) this.color = "red";
    else if( this.progress < 66 ) this.color = "yellow";
    if( this.progress < 100 ) this.color = "green";
    console.log(this.progress);

  }

}
