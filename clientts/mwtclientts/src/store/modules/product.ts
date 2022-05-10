import { VuexModule, Module, Mutation, Action } from "vuex-module-decorators";
import callApi from "@/api/callApi";
import Product from "@/interfaces/product";
import Category from "@/interfaces/category";
import UpdateStock from "@/interfaces/updatestock";
@Module({ namespaced: true })
class Products extends VuexModule {
  products = new Product();
  categories = new Category();
  updateStock = new UpdateStock();

  @Action
  public async AddProduct(data: Product): Promise<any> {
    data.SellerID = Number(localStorage.getItem("UserID"));
    return callApi
      .AsyncPOST("Product/AddProduct", data)
      .then((response) => {
        // console.log("console", response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async UpdateProduct(data: any): Promise<any> {
    return callApi
      .AsyncPOST("Product/UpdateProduct", data)
      .then((response) => {
        // console.log(response);
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async DeleteProduct(id: any): Promise<any> {
    return callApi
      .AsyncGET(`Product/DeleteProduct/${id}`)
      .then((response) => {
        // console.log(response);
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async Getcategory(): Promise<any> {
    return callApi
      .AsyncGET("Product/GetCategories")
      .then((response) => {
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async GetAllProducts(data: any): Promise<any> {
    return callApi
      .AsyncGET("Product/GetMyProducts/" + localStorage.getItem("UserID"))
      .then((response) => {
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async GetProducts(id: any): Promise<any> {
    return callApi
      .AsyncGET(`Product/GetProduct/${id}`)
      .then((response) => {
        // console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async UpdateStock(data: any): Promise<any> {
    return callApi
      .AsyncPOST(`Product/UpdateStock`, data)
      .then((response) => {
        // console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async GetStock(id: any): Promise<any> {
    return callApi
      .AsyncGET(`Product/GetStock/${id}`)
      .then((response) => {
        // console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async GetRandomProduct(): Promise<any> {
    return callApi
      .AsyncGET("Product/GetRandomProducts/12")
      .then((response) => {
        // console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
}

export default Products;
