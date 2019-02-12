import React from 'react';

class Loading extends React.Component {
    constructor(props) {
        super(props);

        this. totalDots = 5;
        this.state = {
            dot: 0,
            direction: -1
        };

        this.update = this.update.bind(this);
    }

    componentDidMount() {
        this.interval = setInterval(this.update, 200);
    }

    componentWillUnmount() {
        clearInterval(this.interval);
    }

    update() {
        let { dot, direction } = this.state;
        if (dot == 0 || dot == this.totalDots - 1) {
            direction = -direction;
        }
        dot += direction;
        this.setState({ dot, direction });
    }

    render() {
        return (
            <div className={'loading' + (this.props.isFixed ? ' fixed' : '')}>
                {[...Array(this.totalDots)].map((o, i) => (
                    <span key={i} className={'dot' + (i === this.state.dot ? ' current' : '')}>.</span>
                ))}
            </div>
        );
    }
}

export default Loading;