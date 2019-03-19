import * as React from 'react';



interface Props {
    pageNo?:number,
    totalPages?:number,
    loadDataCallback? : (pageNo:number) => void
}

export default class Pager extends React.Component<Props,object> {
    render() {
        const {pageNo,totalPages} = this.props;

        return (
        <div>
            <button disabled={(!pageNo || !totalPages || pageNo === 1)} onClick={() => this.onFirstClicked()}>First</button>
            <button disabled={(!pageNo || !totalPages || pageNo === 1)} onClick={() => this.onPrevClicked()}>Prev</button>
            <button disabled={(!pageNo || !totalPages || pageNo === totalPages)} onClick={() => this.onNextClicked()}>Next</button>
            <button disabled={(!pageNo || !totalPages || pageNo === totalPages)} onClick={() => this.onLastClicked()}>Last</button>
        </div>
        );
    }
    onPrevClicked(): void {
        if(this.props.loadDataCallback && this.props.pageNo && this.props.totalPages && this.props.pageNo > 1) {
            this.props.loadDataCallback(this.props.pageNo - 1);
        }
    }
    onNextClicked(): void {
        if(this.props.loadDataCallback && this.props.pageNo && this.props.totalPages && this.props.pageNo < this.props.totalPages ) {
            this.props.loadDataCallback(this.props.pageNo + 1);
        }
    }
    onLastClicked(): void {
        if(this.props.loadDataCallback && this.props.totalPages && this.props.totalPages > 0) {
            this.props.loadDataCallback(this.props.totalPages);
        }
    }

    onFirstClicked(): void {
        if(this.props.loadDataCallback) {
            this.props.loadDataCallback(1);
        }
    }
}