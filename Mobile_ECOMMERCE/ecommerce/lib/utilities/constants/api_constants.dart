class ApiConstants {
  ApiConstants._();

  // Base Url
  static const devBaseUrl = 'http://103.197.184.184:8080';
  static const stgBaseUrl = 'https://api.github.com';
  static const prodBaseUrl = 'https://api.github.com';

  // User
  static const userLogin = '/login/login';
  static const userLogout = '/login/logout';
  static const userMe = '/login/me';
  static const userRegister = '/login/register';

  // Todo
  static const task = '/task';
  static const addTask = '/task';
  static const updateTask = '/task/{id}';
  static const deleteTask = '/task/{id}';

  static const repository = '/repositories/{id}';

  static const authToken = 'token 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';
  static const idRepos = '406010997';
   
  // Home
  static const homeGetListProduct = '/api/Home/get-list-product' ;

  static const nonAuthenticatedPaths = [];
}
