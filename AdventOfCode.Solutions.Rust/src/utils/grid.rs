use std::collections::HashMap;

#[derive(Debug, Clone, Copy, PartialEq, Eq, Hash, Ord, PartialOrd)]
pub enum Direction {
    N = 0,
    E = 1,
    W = 2,
    S = 3,
}

impl Direction {
    pub fn rotate_right_90deg(&self) -> Self {
        match self {
            Direction::N => Direction::E,
            Direction::E => Direction::S,
            Direction::S => Direction::W,
            Direction::W => Direction::N,
        }
    }

    pub fn rotate_left_90deg(&self) -> Self {
        match self {
            Direction::N => Direction::W,
            Direction::E => Direction::N,
            Direction::S => Direction::E,
            Direction::W => Direction::S,
        }
    }
}

#[derive(Debug, Clone, Copy, PartialEq, Eq, Hash, Ord, PartialOrd)]
pub struct Coordinate {
    pub x: isize,
    pub y: isize,
}

#[allow(dead_code)]
impl Coordinate {
    pub fn north(&self) -> Self {
        Self {
            x: self.x,
            y: self.y - 1,
        }
    }

    pub fn south(&self) -> Self {
        Self {
            x: self.x,
            y: self.y + 1,
        }
    }

    pub fn east(&self) -> Self {
        Self {
            x: self.x + 1,
            y: self.y,
        }
    }

    pub fn west(&self) -> Self {
        Self {
            x: self.x - 1,
            y: self.y,
        }
    }

    pub fn north_east(&self) -> Self {
        Self {
            x: self.x + 1,
            y: self.y - 1,
        }
    }

    pub fn north_west(&self) -> Self {
        Self {
            x: self.x - 1,
            y: self.y - 1,
        }
    }

    pub fn south_east(&self) -> Self {
        Self {
            x: self.x + 1,
            y: self.y + 1,
        }
    }

    pub fn south_west(&self) -> Self {
        Self {
            x: self.x - 1,
            y: self.y + 1,
        }
    }

    pub fn neighbour_by_direction(&self, direction: &Direction) -> Self {
        match direction {
            Direction::N => self.north(),
            Direction::E => self.east(),
            Direction::W => self.west(),
            Direction::S => self.south(),
        }
    }

    pub fn neighbours(&self) -> [Self; 8] {
        [
            self.north(),
            self.east(),
            self.west(),
            self.south(),
            self.north_east(),
            self.north_west(),
            self.south_east(),
            self.south_west(),
        ]
    }

    pub fn neighbours_orthogonal(&self) -> [Self; 4] {
        [self.north(), self.east(), self.west(), self.south()]
    }

    pub fn neighbours_diagonal(&self) -> [Self; 4] {
        [
            self.north_east(),
            self.north_west(),
            self.south_east(),
            self.south_west(),
        ]
    }

    pub fn manhattan_distance(&self, other: &Self) -> isize {
        (self.x - other.x).abs() + (self.y - other.y).abs()
    }
}

pub fn parse_grid(input: &str) -> HashMap<Coordinate, char> {
    input
        .lines()
        .enumerate()
        .flat_map(|(y, line)| {
            line.char_indices()
                .map(|(x, ch)| {
                    (
                        Coordinate {
                            x: x as isize,
                            y: y as isize,
                        },
                        ch,
                    )
                })
                .collect::<Vec<_>>()
        })
        .collect()
}

#[allow(dead_code)]
pub fn print_grid(map: &HashMap<Coordinate, char>) {
    let (w, h) = map
        .keys()
        .fold((0, 0), |(x, y), coor| match (coor.x > x, coor.y > y) {
            (true, true) => (coor.x, coor.y),
            (true, false) => (coor.x, y),
            (false, true) => (x, coor.y),
            _ => (x, y),
        });

    for y in 0..h + 1 {
        println!(
            "{}",
            (0..w + 1)
                .map(|x| map[&Coordinate { x, y }])
                .collect::<String>()
        );
    }
}
