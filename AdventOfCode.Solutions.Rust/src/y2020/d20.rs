use std::hash::{DefaultHasher, Hash, Hasher};

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day20::solve_part_one(input),
        Part::P2 => Day20::solve_part_two(input),
    }
}

struct Day20;

#[derive(Clone, PartialEq, Eq, Hash, Debug)]
struct Pattern {
    rows: Vec<String>,
}

impl From<Vec<Vec<Option<Tile>>>> for Pattern {
    fn from(tiles: Vec<Vec<Option<Tile>>>) -> Self {
        let mut rows = vec![];
        let tile_size = tiles[0][0].as_ref().unwrap().pattern.rows.len();

        for row in tiles {
            for i in 0..tile_size {
                let mut line = String::new();
                for tile in &row {
                    line.push_str(&tile.as_ref().unwrap().pattern.rows[i]);
                }
                rows.push(line);
            }
        }

        Self { rows }
    }
}

impl std::fmt::Display for Pattern {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        for row in &self.rows {
            writeln!(f, "{}", row)?;
        }
        Ok(())
    }
}

impl Pattern {
    fn flipped_horizontally(&self) -> Self {
        let mut rows = vec![String::new(); self.rows.len()];

        for (i, line) in self.rows.iter().enumerate() {
            for ch in line.chars().rev() {
                rows[i].push(ch);
            }
        }

        Self { rows }
    }

    fn flipped_vertically(&self) -> Self {
        let mut rows = vec![String::new(); self.rows.len()];

        for (i, line) in self.rows.iter().rev().enumerate() {
            for ch in line.chars() {
                rows[i].push(ch);
            }
        }

        Self { rows }
    }

    fn rotated_clockwise(&self) -> Self {
        let mut rows = vec![String::new(); self.rows.len()];

        for line in self.rows.iter().rev() {
            for (i, ch) in line.char_indices() {
                rows[i].push(ch);
            }
        }

        Self { rows }
    }

    fn orientations(&self) -> [Self; 12] {
        [
            self.clone(),
            self.rotated_clockwise(),
            self.rotated_clockwise().rotated_clockwise(),
            self.rotated_clockwise()
                .rotated_clockwise()
                .rotated_clockwise(),
            self.flipped_horizontally(),
            self.flipped_horizontally().rotated_clockwise(),
            self.flipped_horizontally()
                .rotated_clockwise()
                .rotated_clockwise(),
            self.flipped_horizontally()
                .rotated_clockwise()
                .rotated_clockwise()
                .rotated_clockwise(),
            self.flipped_vertically(),
            self.flipped_vertically().rotated_clockwise(),
            self.flipped_vertically()
                .rotated_clockwise()
                .rotated_clockwise(),
            self.flipped_vertically()
                .rotated_clockwise()
                .rotated_clockwise()
                .rotated_clockwise(),
        ]
    }
}

#[derive(Clone, PartialEq, Eq, Hash, Debug)]
struct Tile {
    id: u64,
    edges: [Edge; 4],
    pattern: Pattern,
    pattern_with_edges: Pattern,
    matches: [Option<u64>; 4],
}

impl From<&str> for Tile {
    fn from(value: &str) -> Self {
        let (title, pattern) = value.split_once("\n").unwrap();
        let id = title[5..9].parse().unwrap();
        let edges = Self::get_hashed_edges(pattern);
        let matches = [None; 4];

        Self {
            id,
            edges,
            pattern: Pattern {
                rows: pattern
                    .lines()
                    .skip(1)
                    .take(8)
                    .map(|line| line[1..9].to_string())
                    .collect::<Vec<_>>(),
            },
            pattern_with_edges: Pattern {
                rows: pattern.lines().map(|line| line.to_string()).collect(),
            },
            matches,
        }
    }
}

impl Tile {
    fn find_exact_match(&self, edge: usize, tiles: &Vec<Tile>) -> Option<Tile> {
        let match_edge = match edge {
            0 => 2,
            1 => 3,
            2 => 0,
            3 => 1,
            _ => unreachable!(),
        };

        let self_edge = &self.edges[edge];

        tiles.iter().find_map(|tile| {
            tile.orientations().into_iter().find(|tile| {
                let match_edge = &tile.edges[match_edge];
                tile.id != self.id && self_edge == match_edge
            })
        })
    }

    fn count_matches(&self, tiles: &Vec<Tile>) -> u8 {
        let mut count = 0;
        for edge in self.edges.iter() {
            if let Some(_) = tiles.iter().find(|other| {
                self.id != other.id
                    && other
                        .edges
                        .iter()
                        .any(|other_edge| other_edge == edge || *other_edge == edge.flipped())
            }) {
                count += 1;
            }
        }

        count
    }

    fn rotated_clockwise(&self) -> Self {
        Self {
            id: self.id,
            edges: [
                self.edges[3].flipped(),
                self.edges[0].clone(),
                self.edges[1].flipped(),
                self.edges[2].clone(),
            ],
            pattern: self.pattern.rotated_clockwise(),
            pattern_with_edges: self.pattern_with_edges.rotated_clockwise(),
            matches: [
                self.matches[3].clone(),
                self.matches[0].clone(),
                self.matches[1].clone(),
                self.matches[2].clone(),
            ],
        }
    }

    fn flipped_horizontally(&self) -> Self {
        Self {
            id: self.id,
            edges: [
                self.edges[0].flipped(),
                self.edges[3].clone(),
                self.edges[2].flipped(),
                self.edges[1].clone(),
            ],
            pattern: self.pattern.flipped_horizontally(),
            pattern_with_edges: self.pattern_with_edges.flipped_horizontally(),
            matches: [
                self.matches[0].clone(),
                self.matches[3].clone(),
                self.matches[2].clone(),
                self.matches[1].clone(),
            ],
        }
    }

    fn flipped_vertically(&self) -> Self {
        Self {
            id: self.id,
            edges: [
                self.edges[2].clone(),
                self.edges[1].flipped(),
                self.edges[0].clone(),
                self.edges[3].flipped(),
            ],
            pattern: self.pattern.flipped_vertically(),
            pattern_with_edges: self.pattern_with_edges.flipped_vertically(),
            matches: [
                self.matches[2].clone(),
                self.matches[1].clone(),
                self.matches[0].clone(),
                self.matches[3].clone(),
            ],
        }
    }

    fn orientations(&self) -> [Self; 12] {
        [
            self.clone(),
            self.rotated_clockwise(),
            self.rotated_clockwise().rotated_clockwise(),
            self.rotated_clockwise()
                .rotated_clockwise()
                .rotated_clockwise(),
            self.flipped_horizontally(),
            self.flipped_horizontally().rotated_clockwise(),
            self.flipped_horizontally()
                .rotated_clockwise()
                .rotated_clockwise(),
            self.flipped_horizontally()
                .rotated_clockwise()
                .rotated_clockwise()
                .rotated_clockwise(),
            self.flipped_vertically(),
            self.flipped_vertically().rotated_clockwise(),
            self.flipped_vertically()
                .rotated_clockwise()
                .rotated_clockwise(),
            self.flipped_vertically()
                .rotated_clockwise()
                .rotated_clockwise()
                .rotated_clockwise(),
        ]
    }

    fn get_hashed_edges(tile: &str) -> [Edge; 4] {
        let lines: Vec<&str> = tile.lines().collect();
        let top = lines.first().unwrap().to_string();
        let bottom = lines.last().unwrap().to_string();
        let left: String = lines
            .iter()
            .map(|line| line.chars().next().unwrap())
            .collect();
        let right: String = lines
            .iter()
            .map(|line| line.chars().last().unwrap())
            .collect();

        [top.into(), right.into(), bottom.into(), left.into()]
    }
}

#[derive(PartialEq, Eq, Clone, Debug, Hash)]
struct Edge {
    hash: [u64; 2],
}

impl From<String> for Edge {
    fn from(value: String) -> Self {
        Self {
            hash: [
                {
                    let mut hasher = DefaultHasher::new();
                    value.hash(&mut hasher);
                    hasher.finish()
                },
                {
                    let mut hasher = DefaultHasher::new();
                    value.chars().rev().collect::<String>().hash(&mut hasher);
                    hasher.finish()
                },
            ],
        }
    }
}

impl Edge {
    fn flipped(&self) -> Self {
        Self {
            hash: [self.hash[1], self.hash[0]],
        }
    }
}

struct SquarePuzzle {
    bounds: usize,
    tiles: Vec<Tile>,
    positions: Vec<Vec<Option<Tile>>>,
    next_tile: Option<Tile>,
    next_position: (usize, usize),
}

impl SquarePuzzle {
    fn new(tiles: Vec<Tile>) -> Self {
        let bounds = (tiles.len() as f64).sqrt() as usize;
        SquarePuzzle {
            bounds,
            tiles,
            positions: vec![vec![None; bounds]; bounds],
            next_tile: None,
            next_position: (0, 0),
        }
    }

    fn find_starting_tile(&self) -> &Tile {
        self.tiles
            .iter()
            .find(|tile| tile.count_matches(&self.tiles) == 2)
            .unwrap()
    }

    fn place_next_tile(&mut self) -> bool {
        if self.next_position == (0, 0) {
            self.place_starting_tile()
        } else if let Some(tile) = &self.next_tile {
            let (y, x) = self.next_position;
            self.positions[y][x] = Some(tile.clone());

            if x == self.bounds - 1 {
                if y < self.bounds - 1 {
                    let leftmost_tile = self.positions[y][0].as_ref().unwrap();
                    let next_tile = leftmost_tile.find_exact_match(2, &self.tiles).unwrap();
                    self.next_tile = Some(next_tile);
                    self.next_position = (y + 1, 0);
                }
            } else {
                let next_tile = tile.find_exact_match(1, &self.tiles).unwrap();
                self.next_tile = Some(next_tile);
                self.next_position = (y, x + 1);
            }

            true
        } else {
            false
        }
    }

    fn place_starting_tile(&mut self) -> bool {
        let starting_tile = self.find_starting_tile();

        if let Some((tile, next_tile)) =
            starting_tile
                .orientations()
                .into_iter()
                .find_map(|orientation| {
                    if let Some(next) = orientation.find_exact_match(1, &self.tiles) {
                        return if orientation.find_exact_match(0, &self.tiles).is_none() {
                            Some((orientation, next))
                        } else {
                            None
                        };
                    } else {
                        None
                    }
                })
        {
            self.positions[0][0] = Some(tile);
            self.next_tile = Some(next_tile);
            self.next_position = (0, 1);
            true
        } else {
            false
        }
    }
}

const SEA_MONSTER: [&str; 3] = [
    "                  # ",
    "#    ##    ##    ###",
    " #  #  #  #  #  #   ",
];

#[allow(unused_variables)]
impl Solution for Day20 {
    fn solve_part_one(input: String) -> String {
        let tiles: Vec<Tile> = input
            .split("\n\n")
            .map(|paragraph| Tile::from(paragraph))
            .collect();

        tiles
            .iter()
            .filter_map(|tile| match tile.count_matches(&tiles) {
                2 => Some(tile.id),
                _ => None,
            })
            .product::<u64>()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        let mut puzzle = SquarePuzzle::new(
            input
                .split("\n\n")
                .map(|paragraph| Tile::from(paragraph))
                .collect(),
        );

        for _ in 0..puzzle.tiles.len() {
            puzzle.place_next_tile();
        }

        let pattern = Pattern::from(puzzle.positions);

        let monsters = pattern.orientations().iter().find_map(|pattern| {
            let mut count = 0;

            for y in 0..pattern.rows.len() - SEA_MONSTER.len() {
                for x in 0..pattern.rows[y].len() - SEA_MONSTER[0].len() {
                    let mut monster = true;
                    for (my, sea_monster_row) in SEA_MONSTER.iter().enumerate() {
                        for (mx, sea_monster_char) in sea_monster_row.char_indices() {
                            if sea_monster_char == '#'
                                && pattern.rows[y + my].chars().nth(x + mx) != Some('#')
                            {
                                monster = false;
                                break;
                            }
                        }

                        if !monster {
                            break;
                        }
                    }

                    if monster {
                        count += 1;
                    }
                }
            }

            if count == 0 {
                None
            } else {
                Some(count)
            }
        });

        let sea_monster_habitat = monsters.unwrap()
            * SEA_MONSTER.iter().fold(0, |habitat, row| {
                habitat + row.chars().filter(|&ch| ch == '#').count()
            });
        let roughness = pattern.rows.iter().fold(0, |roughness, row| {
            roughness + row.chars().filter(|&ch| ch == '#').count()
        });

        (roughness - sea_monster_habitat).to_string()
    }
}
