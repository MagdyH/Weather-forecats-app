import React, { Component } from 'react';
import ReactPullToRefresh from 'react-pull-to-refresh';
import { resolveModuleName } from 'typescript';
import { PullToRefresh, PullDownContent, ReleaseContent, RefreshContent } from "react-js-pull-to-refresh";

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { forecasts: [], loading: true };
        this.handlePullRefresh = this.handlePullRefresh.bind(this);
        this.onRefresh = this.onRefresh.bind(this);
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    handlePullRefresh() {
        this.populateWeatherData();
    }

    onRefresh() {
        return new Promise((resolve) => {
            
            this.populateWeatherData();
            setTimeout(resolve, 2000);
        });
    }

     renderForecastsTable(forecasts) {
        return (
            <PullToRefresh
                pullDownContent={<PullDownContent />}
                releaseContent={<ReleaseContent />}
                refreshContent={<RefreshContent />}
                pullDownThreshold={200}
                onRefresh={this.onRefresh}
                triggerHeight={50}
                backgroundColor='white'
                startInvisible={true}
            >
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Weather State</th>
                            <th>Image</th>
                        </tr>
                    </thead>
                    <tbody>
                        {forecasts.map(forecast => {
                            let date = new Date(forecast.applicable_Date);
                            let nowDate = new Date();
                            let dateString = date.getDay() - nowDate.getDay() == 1 ? 'Tomorrow' : date.toDateString();
                            return (<tr key={forecast.applicable_Date}>
                                <td>{dateString}</td>
                                <td>{forecast.weather_State_Name}</td>
                                <td><img className={'state-icon-sml'} src={forecast.image_Url} /></td>
                                {/*<td><div className={'state-icon-sml'} style={{ background: `url(${forecast.image_Url})` }}></div></td>*/}
                            </tr>)
                        })}
                    </tbody>
                </table>
            </PullToRefresh>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <h1 id="tabelLabel" >Belfast Weather forecast</h1>
                <p>This is weather forecast for the next 5 days</p>
                {contents}
            </div>
        );
    }

    async populateWeatherData() {
        const response = await fetch('api/v1/weatherforecast', {
            headers: {
                'Content-Type': 'application/json',
                'ApiKey': '803988a8-5915-4d8c-b966-86652b54a17a'
            }
        });
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }
}
