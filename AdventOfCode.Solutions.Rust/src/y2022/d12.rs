use crate::core::{Part, Solution};

use super::coor::Position;

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day12::solve_part_one(input),
        Part::P2 => Day12::solve_part_two(input),
    }
}

impl Position<usize> {
    fn tuple(&self) -> (usize, usize) {
        (self.x, self.y)
    }
}

struct SquareMap<T> {
    lowlands: Vec<Position<usize>>,
    items: Vec<Vec<T>>,
    start: Position<usize>,
    end: Position<usize>,
    rows: usize,
    cols: usize,
}

impl SquareMap<u8> {
    fn new(input: String) -> Self {
        let mut lowlands = vec![];
        let mut start = Position { x: 0, y: 0 };
        let mut end = Position { x: 0, y: 0 };
        let mut rows = 0;
        let mut cols = 0;

        let items = input
            .lines()
            .enumerate()
            .map(|(x, line)| {
                if x > rows {
                    rows = x;
                }

                line.chars()
                    .enumerate()
                    .map(|(y, ch)| {
                        if y > cols {
                            cols = y;
                        }

                        let pos = Position { x, y };

                        match ch {
                            'S' => {
                                lowlands.push(pos);
                                start = pos;
                                b'a'
                            }
                            'E' => {
                                end = pos;
                                b'z'
                            }
                            'a' => {
                                lowlands.push(pos);
                                ch as u8
                            }
                            'b'..='z' => ch as u8,
                            _ => panic!(),
                        }
                    })
                    .collect()
            })
            .collect();

        Self {
            lowlands,
            items,
            start,
            end,
            rows,
            cols,
        }
    }

    fn find_lengths(&self) -> Vec<Vec<i32>> {
        let mut queue = vec![self.end];
        let mut distances = vec![vec![0; self.cols + 1]; self.rows + 1];
        distances[self.end.x][self.end.y] = 1;
        while queue.len() > 0 {
            let pos = queue.pop().unwrap();
            let (x, y) = pos.tuple();
            let steps = distances[x][y];

            // println!("{:?}", pos.tuple());

            for adj in self.peek_around(pos) {
                let (x, y) = adj.tuple();

                // println!("{:?} vs {:?} = {}", pos.tuple(), adj.tuple(), self.get(pos) <= self.get(adj) + 1);

                if distances[x][y] == 0 && self.get(pos) <= self.get(adj) + 1 {
                    distances[x][y] = steps + 1;
                    queue.insert(0, adj);
                }
            }
        }

        distances
    }

    fn peek_around(&self, pos: Position<usize>) -> Vec<Position<usize>> {
        let mut adjacent = vec![];
        let (x, y) = pos.tuple();

        if x > 0 {
            adjacent.push(Position { x: x - 1, y });
        }

        if x < self.rows {
            adjacent.push(Position { x: x + 1, y });
        }

        if y > 0 {
            adjacent.push(Position { x, y: y - 1 });
        }

        if y < self.cols {
            adjacent.push(Position { x, y: y + 1 });
        }

        adjacent
    }

    fn get(&self, pos: Position<usize>) -> u8 {
        let (x, y) = pos.tuple();
        self.items[x][y]
    }
}

struct Day12;
impl Solution for Day12 {
    fn solve_part_one(input: String) -> String {
        let map = SquareMap::new(input);
        let lengths = map.find_lengths();
        let (x, y) = map.start.tuple();

        (lengths[x][y] - 1).to_string()
    }

    fn solve_part_two(input: String) -> String {
        let map = SquareMap::new(input);
        let lengths = map.find_lengths();

        (map.lowlands.iter().fold(i32::MAX, |closest, pos| {
            let (x, y) = pos.tuple();
            let steps = lengths[x][y];

            // println!("{:?}", pos.tuple());

            if steps < closest && steps != 0 {
                steps
            } else {
                closest
            }
        }) - 1)
            .to_string()
    }
}
