import 'package:dio/dio.dart' hide Headers;
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
}
