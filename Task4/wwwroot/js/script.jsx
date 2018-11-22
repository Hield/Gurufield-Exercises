class WatchlistBox extends React.Component {
    constructor (props) {
        super(props);
        this.state = { data: [], searchString: '' };
        this.handleSearchStringChange = this.handleSearchStringChange.bind(this);
        this.handleMovieEdit = this.handleMovieEdit.bind(this);
    }
    loadMoviesFromServer(searchString) {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url + "?searchString=" + searchString, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }
    componentDidMount() {
        this.loadMoviesFromServer(this.state.searchString);
        window.setInterval(
            () => this.loadMoviesFromServer(this.state.searchString),
            this.props.pollInterval,
        )
    }
    handleSearchStringChange(e) {
        this.setState({ searchString: e.target.value });
        this.loadMoviesFromServer(e.target.value);
    }
    handleMovieEdit(movie) {
        console.log(movie);
        const data = new FormData();
        data.append('id', movie.id);
        data.append('name', movie.name);
        data.append('releaseYear', movie.releaseYear);
        data.append('genre', movie.genre);
        
        const xhr = new XMLHttpRequest();
        xhr.open('post', this.props.editUrl, true);
        xhr.onload = () => {
            this.loadMoviesFromServer(this.state.searchString);
        }
        xhr.send(data);
    }
    render() {
        return (
            <div className="watchlistBox">
                <h1>Movie Watchlist</h1>
                <input 
                    type="text" 
                    placeholder="Search" 
                    value={this.state.searchString}
                    onChange={this.handleSearchStringChange}
                />
                <MovieList 
                    data={this.state.data} 
                    onMovieEdit={this.handleMovieEdit}
                />
            </div>
        );
    }
}

class MovieList extends React.Component {
    render() {
        const movieNodes = this.props.data.map((movie, index) => (
            <Movie 
                name={movie.name} 
                key={movie.id}
                id={movie.id}
                index={index}
                releaseYear={movie.releaseYear}
                genre={movie.genre}
                onMovieEdit={this.props.onMovieEdit}
            />
        ));
        return (
            <table className="movieList table">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Name</th>
                        <th scope="col">Release Year</th>
                        <th scope="col">Genre</th>
                        <th scope="col">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {movieNodes}
                </tbody>
            </table>
        );
    }
}

class Movie extends React.Component {
    constructor(props) {
        super(props);
        this.state = { 
            editMode: false, 
            changedName: this.props.name,
            changedYear: this.props.releaseYear, 
            changedGenre: this.props.genre
        };
        this.goToEditMode = this.goToEditMode.bind(this);
        this.getOutOfEditMode = this.getOutOfEditMode.bind(this);
        this.handleEditMovie = this.handleEditMovie.bind(this);
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handleYearChange = this.handleYearChange.bind(this);
        this.handleGenreChange = this.handleGenreChange.bind(this);
    }
    goToEditMode() {
        this.setState({ editMode: true });
    }
    getOutOfEditMode() {
        this.setState({ editMode: false });
    }
    handleEditMovie() {
        this.props.onMovieEdit({
            id: this.props.id,
            name: this.state.changedName,
            releaseYear: this.state.changedYear,
            genre: this.state.changedGenre
        });
        
        this.getOutOfEditMode();
    }
    handleNameChange(e) {
        this.setState({ changedName: e.target.value });
    }
    handleYearChange(e) {
        this.setState({ changedYear: e.target.value });
    }
    handleGenreChange(e) {
        this.setState({ changedGenre: e.target.value });
    }
    render() {
        if (this.state.editMode) {
            return (
                <tr className="movie">
                    <th scope="row">{this.props.index + 1}</th>
                    <td>
                        <input 
                            type="text" 
                            //placeholder={this.props.name}
                            value={this.state.changedName}
                            onChange={this.handleNameChange}
                        />
                    </td>
                    <td>
                        <input 
                            type="text" 
                            //placeholder={this.props.releaseYear}
                            value={this.state.changedYear}
                            onChange={this.handleYearChange}
                        />
                    </td>
                    <td>
                        <input 
                            type="text" 
                            //placeholder={this.props.genre}
                            value={this.state.changedGenre}
                            onChange={this.handleGenreChange}
                        />
                    </td>
                    <td>
                        <button type="button" className="btn btn-outline-success" onClick={this.handleEditMovie}>Done!</button>
                        <button type="button" className="btn btn-outline-secondary" onClick={this.getOutOfEditMode}>Cancel</button>
                    </td>
                </tr>
            )
        }
        return (
            <tr className="movie">
                <th scope="row">{this.props.index + 1}</th>
                <td>{this.props.name}</td>
                <td>{this.props.releaseYear}</td>
                <td>{this.props.genre}</td>
                <td>
                    <button type="button" className="btn btn-outline-primary" onClick={this.goToEditMode}>Edit</button>
                </td>
            </tr>
        );
    }
}

ReactDOM.render(
    <WatchlistBox 
        url="/movies" 
        editUrl="/movies/edit"
        pollInterval={2000} 
    />,
    document.getElementById('content')
);