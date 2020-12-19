import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'convertRarity'
})

// use it in an input field
// {{ 'common' | convertRarity }}
export class ConvertRarityPipe implements PipeTransform {

  transform(value:any): any{
    if(value === 'common') return 1;
    if(value === 'uncommon') return 2;
    if(value === 'rare') return 3;   
  }
}
