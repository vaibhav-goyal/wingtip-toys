import * as React from 'react';
import ProductList from './ProductList';
import Pager from './Pager';
import Product from '../DTOs/Product';
import PageResult from '../DTOs/PageResult';
import ProductService from '../services/ProductService';

interface State {
    data?: PageResult<Product>,
    isLoading: boolean
}

export default class ProductListPage extends React.Component<{}, State> {
    constructor(props: {}) {
        super(props);
        this.state = { data: undefined, isLoading: false };
    }

    componentDidMount() {
        const service = new ProductService();
        const request = service.getProducts(1, 1, 4);
        this.setState({ isLoading: true });
        request.then(result => {
            this.setState({ data: result, isLoading: false });
        });
    }

    render() {

        const { data, isLoading } = this.state;
        let child: JSX.Element;
        if (!isLoading && data) {
            child =
                <div>
                    <ProductList items={data.items}></ProductList>
                    <Pager pageNo={data.pageNo} totalPages={data.totalPages} loadDataCallback={(pageNo) => this.loadData(pageNo)}></Pager>
                </div>;
        } else {
            child = <span>Loading...</span>;
        }
        return (
            <section className="featured">
                <div className="content-wrapper">
                    <hgroup className="title">
                        <h1>Products</h1>
                    </hgroup>
                    <section className="featured">
                        <div>
                            <table>
                                <tbody>
                                    <tr>
                                        <td style={{ "width" :"30%", "vertical-align":"top"}}>
                                            <hgroup className="title">
                                                <h1>Wingtip Toys</h1>
                                                <h2>Wingtip Toys can help you find the perfect gift</h2>
                                            </hgroup>
                                            <p>
                                                We're all about transportation toys. You can order
                                        any of our toys today. Each toy listing has detailed
                                        information to help you choose the right toy.
                                        </p>
                                        </td>
                                        <td>
                                            {child}
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </section>
                </div>
            </section>
        );
    }

    loadData(pageNo: number): void {
        const service = new ProductService();
        const request = service.getProducts(1, pageNo, 4);
        this.setState({ isLoading: true });
        request.then(result => {
            this.setState({ data: result, isLoading: false });
        });
    }
}