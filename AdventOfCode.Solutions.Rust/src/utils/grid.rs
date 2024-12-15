use std::collections::HashMap;

#[derive(Debug)]
pub enum Direction {
    N,
    E,
    W,
    S,
}

#[derive(Debug, Clone, Copy, PartialEq, Eq, Hash)]
pub struct Coordinate {
    pub x: isize,
    pub y: isize,
}

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
