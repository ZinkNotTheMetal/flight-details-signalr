import { useState } from "react"
import './Search.css'

interface SearchProps {
  onSearch: (query: string) => void
}

export const Search = ({ onSearch }: SearchProps) => {
  const [query, setQuery] = useState<string>('');

  const handleKeyPress = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      onSearch(query);
    }
  };

  return (
    <div className="searchBox">
      <input
        type="text"
        className="searchInput"
        placeholder="Search flights by number, route, or airline..."
        value={query}
        onChange={(e) => setQuery(e.target.value)}
        onKeyDown={handleKeyPress}
      />
    </div>
  );
};