import 'package:dio/dio.dart' hide Headers;
import 'package:ecommerce/data/model/order/order.dart';
import 'package:retrofit/retrofit.dart';

import '../../../utilities/constants/api_constants.dart';
import '../home /home_get_product.dart';

part 'api_client.g.dart';

@RestApi(baseUrl: ApiConstants.devBaseUrl)
abstract class ApiClient {
  factory ApiClient(
    Dio dio,
  ) = _ApiClient;
  @GET(ApiConstants.homeGetListProduct)
  Future<HomeGetProduct> getFilm();

  @GET(ApiConstants.homeGetProductDetail)
  Future<HomeGetProduct> getProductDetail(
    @Path() int droductId,
  );

  @POST(ApiConstants.postOrder)
  Future<Order> postOrder(
    @Body() Order order,
    @Header('Authorization') String token,
  );
}
