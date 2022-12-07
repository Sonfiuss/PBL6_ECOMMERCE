import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/_services/product.service';


@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {

  constructor(
    private  productService : ProductService
  ) { }

  ngOnInit(): void {

  }
  productData:any;
  price =399000;
  overview= 'Sơ mi là một trong những loại phục trang có tính ứng dụng cao, bạn có thể sử dụng trong những bữa tiệc yêu cầu sự lịch lãm, hoặc những chuyến dạo chơi thoải mái bên bạn bè, hoặc ngay cả dịp hẹn hò cùng người yêu. Cùng Gavin tìm hiểu những tiêu chí khi chọn mua áo sơ mi sau đây.';
  qty=1;
  sum_price =this.price * this.qty;
  currentDate = new Date();

  src = [
    {img_id: 1,url:"https://cf.shopee.vn/file/d4fbe0e60caeb997e1bd24126ccec5cf"},
    {img_id: 2,url:"https://dony.vn/wp-content/uploads/2021/07/ao-so-mi-nu-ban-nhieu-trn-tang-tmdt-viet-1.jpg"},
    {img_id: 3,url:"https://lzd-img-global.slatic.net/g/p/0f2d9ede901e0bcd123715e4f7daad29.jpg_720x720q80.jpg_.webp"},
    {img_id: 4,url:"https://triscy.com/wp-content/uploads/2020/02/1f510db48e651cef5d72860c7b9d43b9.jpg"}
  ]
  ImagePath = 'https://cf.shopee.vn/file/d4fbe0e60caeb997e1bd24126ccec5cf';

  Imagehover(n:any) {
    var filter_array = this.src.filter(x => x.img_id == n);
    filter_array.forEach(element => {this.ImagePath = element.url
    });
  }

  QtyDecrease(n:any){
    if( n > 0 && n!=1){
      this.qty = n-1;
      this.sum_price -= this.price;
    }
    else{
      this.qty = 1;
    }
  }

  QtyIncrease(n:any){
    if(n > 0){
      this.qty = n+1;
      this.sum_price += this.price;
    }
    else{
      this.qty = 1;
    }
  }
}
