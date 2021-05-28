import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IOrder } from 'src/app/shared/models/order';
import { BreadcrumbService } from 'xng-breadcrumb';
import { OrdersService } from '../orders.service';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss']
})
export class OrderDetailComponent implements OnInit {
  order: IOrder;

  constructor(
    private orderService: OrdersService,
    private bcService: BreadcrumbService,
    private activatedRoute: ActivatedRoute) {
      this.bcService.set('@orderDetail', ' ');
     }

  ngOnInit(): void {
    this.loadOrder();
  }

  loadOrder(): void {
    this.orderService.getOrderDetail(+this.activatedRoute.snapshot.paramMap.get('id'))
    .subscribe((order: IOrder) => {
      this.order = order;
      this.bcService.set('@orderDetail', 'Order # ' + String(order.id) + ' - ' + order.status);
    }, error => {
      console.log(error);
    });
  }

}
