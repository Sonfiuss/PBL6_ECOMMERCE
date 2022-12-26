
import { Component, EventEmitter, Input, OnInit, Output ,ViewEncapsulation } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ShoporderdetailService } from 'src/app/_services/shoporderdetail.service';


@Component({
  selector: 'app-manage-order',
  templateUrl: './manage-order.component.html',
  styleUrls: ['./manage-order.component.css'],
  encapsulation: ViewEncapsulation.None,
  styles: [
		`
			.dark-modal .modal-content {
				background-color: #292b2c;
				color: white;
			}
			.dark-modal .close {
				color: white;
			}
			.light-blue-backdrop {
				background-color: #5cb3fd;
			}
		`,
  ]
})
export class ManageOrderComponent implements OnInit {
  @Input() orderdetails: any;
  listOrderDetails : any;
  closeResult: string;
  mode = false;
  constructor(
    private modalService: NgbModal,
    private shopOrderDetailService: ShoporderdetailService ) { }

  ngOnInit(): void {
    this.loadOrderDetails();
  }
  modeOpen(id : any){
    
  }
  show_detail(content:any, id: any){
    console.log(id);
    console.log("HULLO")
    this.modalService.open(content, { size: 'lg' });
  }
  loadOrderDetails(){
    this.shopOrderDetailService.getOrderDetails()
        .subscribe((res) => this.handleGetOrderDetailsSuccess(res),
        (err) => this.handleGetOrderDetailsError(err))
  }
  handleGetOrderDetailsError(err: any){
    console.log(err);
    console.log("thinhnguyen1233456");
  }
  handleGetOrderDetailsSuccess(res: any){
    
    this.orderdetails = res.result.data.filter((x:any) => x.sate == 1)
    
    console.log(res)
  }
  viewall(){

  }
  viewstock(){

  }
}

