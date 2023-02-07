import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrls: ['./paging-header.component.scss']
})
export class PagingHeaderComponent {

@Input() pageSize?: number;
@Input() pageNumber?: number;
@Input() totalCount?: number;


// Used the below to check the values -- when totalcount was 0 then the 0 results message
// was not displaying because the results message is within the element that contains an 
// ngIf checking if the props exist and a value of 0 is falsy

// ngOnChanges(changes: SimpleChanges): void {
//   this.showInputs();
// }

// showInputs(){
//   console.log(this.pageSize);
//   console.log(this.pageNumber);
//   console.log(this.totalCount);
// }

}
